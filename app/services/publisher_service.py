from fastapi import HTTPException
from sqlalchemy import text

from app.db.connection import get_connection
from app.models.publisher import Publisher, PublisherPostForm

async def get_publishers() -> list[Publisher]:
    conn = get_connection()
    query = "SELECT * FROM publishers"
    result = conn.execute(text(query))
    publishers = []
    for row in result:
        publishers.append(Publisher(id=row[0], name=row[1]))
    conn.close()
    return publishers

async def get_publisher_by_id(id: int) -> Publisher:
    conn = get_connection()
    query = "SELECT * FROM publishers WHERE id = :id"
    result = conn.execute(text(query), id=id)
    row = result.fetchone()
    conn.close()
    return Publisher(id=row[0], name=row[1])

async def get_publisher_name(id: int) -> str:
    conn = get_connection()
    query = "SELECT name FROM publishers WHERE id = :id"
    params = {"id": id}
    result = conn.execute(text(query), params)
    publisher = result.fetchone()
    conn.close()
    if publisher != None: 
        publisher = publisher[0]

    return publisher

async def get_publisher_id(name: str) -> Publisher:
    conn = get_connection()
    query = "SELECT * FROM publishers WHERE name = :name"
    params = {"name": name}
    result = conn.execute(text(query), params)
    publisher = result.fetchone()
    conn.close()
    if publisher != None: 
        publisher = publisher[0]

    return publisher

async def create_publisher(publisher: PublisherPostForm) -> Publisher:
    conn = get_connection()
    #check if exists
    publisher_id = await get_publisher_id(publisher.name)
    if publisher_id != None:
        raise HTTPException(status_code=409, detail="Publisher already exists")

    query = "INSERT INTO publishers (name) VALUES (:name) RETURNING id"
    params = {"name": publisher.name}
    result = conn.execute(text(query), params)
    conn.close()
    publisher_id = result.fetchone()[0]
    return Publisher(id=publisher_id, name=publisher.name)

__all__ = [ "get_publishers", "get_publisher_by_id", "get_publisher_by_name", "post_publisher" ]