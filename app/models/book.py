from pydantic import BaseModel
from datetime import date
from typing import List

class BookDesc(BaseModel):
    id: int
    title: str
    author: str
    publisher: str
    genre: List[str] | None
    data_published: date | None
    data_acquired: date | None
    is_read: bool
    pages: int | None

class Book(BaseModel):
    id: int
    title: str
    author_id: int
    publisher_id: int
    genre: List[int] | None
    data_published: date | None
    data_acquired: date | None
    is_read: bool
    pages: int | None

class BookPostForm(BaseModel):
    title: str
    author_id: int
    publisher_id: int
    genre: List[int] | None
    data_published: date | str | None
    data_acquired: date | str | None
    is_read: bool = False
    pages: int = None
    # wishtlist fields
    is_wishlist: bool = False
    priority: int | None

    # transform "on" True in is_wishlist and is_read if is string, if not, just return is_wishlist and is_read
    def __init__(self, **data):
        if data.get('is_wishlist') == 'on':
            data['is_wishlist'] = True
        if data.get('is_read') == 'on':
            data['is_read'] = True
        super().__init__(**data)