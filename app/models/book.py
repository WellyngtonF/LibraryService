from pydantic import BaseModel
from datetime import date
from typing import List

class Book(BaseModel):
    id: int
    title: str
    author: str
    publisher: str
    genre: List[str] | None
    data_published: date | None
    data_acquired: date | None
    is_read: bool
    pages: int | None

class BookPostForm(BaseModel):
    title: str
    author: str
    publisher: str
    genre: List[str] | None
    data_published: date
    data_acquired: date = None 
    is_read: bool = False # default value
    pages: int = None
    # wishtlist fields
    is_wishlist: bool | None
    priority: int | None