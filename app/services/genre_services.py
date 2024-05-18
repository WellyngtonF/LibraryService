from app.db.connection import get_connection
from sqlalchemy import text

from app.models.genre import GenrePostForm, Genre

def get_genres() -> list[Genre]:
    conn = get_connection()
    query = "SELECT * FROM genres"
    result = conn.execute(text(query))
    genres = []
    for genre in result.fetchall():
        genres.append(Genre(id=genre[0], name=genre[1]))
    conn.close()
    return genres

def create_genre(genre: GenrePostForm):
    conn = get_connection()
    params = {"genre": genre.name}
    query = "INSERT INTO genres (name) VALUES (:genre) RETURNING id"
    result = conn.execute(text(query), params)
    genre_id = result.fetchone()[0]
    conn.close()
    return {"id": genre_id, **genre.model_dump()}

def get_genre_id(genre: str):
    conn = get_connection()
    query = "SELECT id FROM genres WHERE name=:name"
    params = {"name": genre}
    result = conn.execute(text(query), params)
    genre_id = result.fetchone()
    if genre_id is None:
        result = create_genre(GenrePostForm(name=genre))
        genre_id = result["id"]
    else:
        genre_id = genre_id[0]
    conn.close()
    return genre_id

def insert_genres(genres: list[int], book_id: int):
    conn = get_connection()
    for genre_id in genres:
        params = {"book_id": book_id, "genre_id": genre_id}
        query = "INSERT INTO book_genres (book_id, genre_id) VALUES (:book_id, :genre_id)"
        conn.execute(text(query), params)
    conn.close()

def get_genres_from_book(book_id: int):
    conn = get_connection()
    query = "SELECT g.name FROM genres g JOIN book_genres bg ON g.id = bg.genre_id WHERE bg.book_id=:book_id"
    params = {"book_id": book_id}
    result = conn.execute(text(query), params)
    genres = [row[0] for row in result]
    conn.close()
    return genres

def get_genres_id_from_book(book_id: int) -> list[int]:
    conn = get_connection()
    query = "SELECT genre_id FROM book_genres WHERE book_id=:book_id"
    params = {"book_id": book_id}
    result = conn.execute(text(query), params)
    genres = [row[0] for row in result]
    conn.close()
    return genres


__all__ = ["get_genres", "get_genre_id", "insert_genres", "create_genre"]