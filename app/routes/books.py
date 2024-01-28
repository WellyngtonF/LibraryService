from fastapi import APIRouter, HTTPException, File, UploadFile
from app.services.book_services import *
from app.models.book import BookPostForm, Book

router = APIRouter()

@router.get("/books", tags=["books"])
async def get_books_route() -> list[Book]:
    result = await get_books()
    return result

@router.post("/books", tags=["books"])
async def create_book_route(book: BookPostForm):
    return await create_book(book)