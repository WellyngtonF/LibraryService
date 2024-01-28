from app.db.connection import get_connection
from sqlalchemy import text

from app.models.genre import GenrePostForm

def get_genres():
    conn = get_connection()
    query = "SELECT * FROM genres"
    result = conn.execute(text(query))
    genres = [dict(row) for row in result]
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

def insert_genres(genres: [str], book_id: int):
    conn = get_connection()
    for genre in genres:
        genre_id = get_genre_id(genre)
        params = {"book_id": book_id, "genre_id": genre_id}
        query = "INSERT INTO book_genres (book_id, genre_id) VALUES (:book_id, :genre_id)"
        conn.execute(text(query), params)
    conn.close()


__all__ = ["get_genres", "get_genre_id", "insert_genres", "create_genre"]