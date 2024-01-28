from fastapi import HTTPException
from app.models.author import AuthorPostForm, Author
from sqlalchemy import text
from app.db.connection import get_connection

async def get_authors() -> list[Author]:
    conn = get_connection()
    query = "SELECT * FROM authors"
    result = conn.execute(text(query))
    authors = result.fetchall()
    conn.close()
    result = []
    for author in authors:
        _author = Author(
            id=author[0],
            name=author[1]
        )
        result.append(_author)
    return result

async def get_author(author_id: int) -> Author:
    conn = get_connection()
    query = "SELECT * FROM authors WHERE id=:author_id"
    params = {"author_id": author_id}
    result = conn.execute(text(query), params)
    author = result.fetchone()
    conn.close()
    _author = Author(
        id=author[0],
        name=author[1]
    )
    return _author

async def get_author_id(author: str) -> int:
    conn = get_connection()
    query = "SELECT id FROM authors WHERE name=:author"
    params = {"author": author}
    result = conn.execute(text(query), params)
    author_id = result.fetchone()
    if author_id != None:
        author_id = author_id[0]
    conn.close()
    return author_id

async def create_author(authorBody: AuthorPostForm) -> Author:
    # check if author already exists
    author = await get_author_id(authorBody.name)
    if author != None:
        raise HTTPException(status_code=409, detail="Author already exists")

    conn = get_connection()
    query = "INSERT INTO authors (name) VALUES (:name) returning id"
    params = {"name": authorBody.name}
    result = conn.execute(text(query), params)
    author_id = result.fetchone()[0]
    conn.close()
    _author = Author(
        id=author_id,
        name=authorBody.name
    )
    return _author