from pydantic import BaseModel

class Wishlist(BaseModel):
    id: int
    book: str
    priority: int

class WishlistPostForm(BaseModel):
    book_id: int
    priority: int