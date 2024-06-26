from fastapi import HTTPException
from sqlalchemy import text

from app.db.connection import get_connection

from app.models.book import BookPostForm, BookDesc, Book
from app.models.wishlist import WishlistPostForm

from app.services.genre_services import insert_genres, get_genres_id_from_book
from app.services.author_service import get_author
from app.services.publisher_service import get_publisher_name, get_publisher_by_id
from app.services.wishlist_service import create_wishlist as create_wishlist

async def get_books() -> list[BookPostForm]:
    conn = get_connection()
    query = "SELECT * FROM books"
    result = conn.execute(text(query))
    books = result.fetchall()
    
    conn.close()
    #convert to a list of dictionaries
    result = []
    for book in books:
        author = await get_author(book[1])
        _book = BookDesc(
            id=book[0],
            author= author.name,
            publisher= await get_publisher_name(book[2]),
            title=book[3],
            genre=None,
            data_published=book[4],
            data_acquired=book[5],
            is_read=book[6],
            pages=book[7]
        )
        result.append(_book)
    return result

async def get_book(id: int) -> Book:
    conn = get_connection()
    query = "SELECT * FROM books WHERE id=:id"
    params = {"id": id}
    result = conn.execute(text(query), params)
    book = result.fetchone()
    if book is None:
        raise HTTPException(status_code=404, detail="Book not found")
    conn.close()
    result = Book(
        id=book[0],
        author_id= book[1],
        publisher_id= book[2],
        title=book[3],
        genre=get_genres_id_from_book(id),
        data_published=book[4],
        data_acquired=book[5],
        is_read=book[6],
        pages=book[7]
    )
    return result

def get_book_id(title: str, author_id: int, publisher_id: int) -> int:
    conn = get_connection()
    query = "SELECT id FROM books WHERE title=:title AND author_id=:author_id AND publisher_id=:publisher_id"
    params = {"title": title, "author_id": author_id, "publisher_id": publisher_id}
    result = conn.execute(text(query), params)
    book_id = result.fetchone()
    if book_id is None:
        return None
    else:
        return book_id[0]

async def create_book(book: BookPostForm) -> BookDesc:
    # check if author already exists, if not create it
    if(await get_author(book.author_id) == None):
        raise HTTPException(status_code=404, detail="Author not found")

    if (await get_publisher_by_id(book.publisher_id) == None):
        raise HTTPException(status_code=404, detail="Publisher not found")

    book_id = get_book_id(book.title, book.author_id, book.publisher_id)
    if book_id != None:
        raise HTTPException(status_code=409, detail="Book already exists")
    
    conn = get_connection()
    query = "INSERT INTO books (title, author_id, publisher_id, data_published, data_acquired, is_read, pages) VALUES (:title, :author, :publisher, :data_published, :data_acquired, :is_read, :pages) RETURNING id"
    params = {"title": book.title, "author": book.author_id, "publisher": book.publisher_id, "data_published": book.data_published, "data_acquired": book.data_acquired, "is_read": book.is_read, "pages": book.pages}
    result = conn.execute(text(query), params)
    book_id = result.fetchone()[0]
    conn.close()
    insert_genres(book.genre, book_id)

    if book.is_wishlist:
        await create_wishlist(WishlistPostForm(book_id=book_id, priority=book.priority))

    result = BookDesc(
        id=book_id,
        author=book.author,
        publisher=book.publisher,
        title=book.title,
        genre=book.genre,
        data_published=book.data_published,
        data_acquired=book.data_acquired,
        is_read=book.is_read,
        pages=book.pages
    )

    return result


__all__ = ["get_books", "create_book", "get_book"]