from sqlalchemy import text

from app.db.connection import get_connection
from app.models.wishlist import Wishlist, WishlistPostForm
from app.models.book import BookDesc

import app.services.book_services as bs

async def get_wishlist() -> list[Wishlist]:
    conn = get_connection()
    query = """
        SELECT W.id, B.title, W.priority
        FROM wishlist W
        INNER JOIN books B ON B.id = W.book_id"""
    result = conn.execute(text(query))
    wishlist = result.fetchall()
    conn.close()
    result = []
    for item in wishlist:
        _item = Wishlist(
            id=item[0],
            book=item[1],
            priority=item[2]
        )
        result.append(_item)
    return result

async def get_wishlist_by_priority(priority: int) -> list[Wishlist]:
    conn = get_connection()
    query = """
        SELECT W.id, B.title, W.priority
        FROM wishlist W
        INNER JOIN books B ON B.id = W.book_id
        WHERE W.priority = :priority"""
    params = {"priority": priority}
    result = conn.execute(text(query), params)
    wishlist = result.fetchall()
    conn.close()
    result = []
    for item in wishlist:
        _item = Wishlist(
            id=item[0],
            book=item[1],
            priority=item[2]
        )
        result.append(_item)
    return result

async def create_wishlist(wishlist: WishlistPostForm) -> Wishlist:
    #check if book exists
    book = await bs.get_book(wishlist.book_id)

    conn = get_connection()
    query = "INSERT INTO wishlist (book_id, priority) VALUES (:book_id, :priority) RETURNING id"
    
    params = {"book_id": wishlist.book_id, "priority": wishlist.priority}
    result = conn.execute(text(query), params)
    wishlist_id = result.fetchone()[0]
    conn.close()
    
    return Wishlist(id=wishlist_id, book=book.title, priority=wishlist.priority)

async def delete_wishlist(id: int):
    conn = get_connection()
    query = "DELETE FROM wishlist WHERE id = :id"
    params = {"id": id}
    conn.execute(text(query), params)
    conn.close()