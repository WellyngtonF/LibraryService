from fastapi import APIRouter, Request, Header, HTTPException
from fastapi.templating import Jinja2Templates
from typing import Annotated
from app.models.book import BookPostForm, BookDesc, Book

from app.services.book_services import *

router = APIRouter()
templates = Jinja2Templates(directory="app/templates/books")

@router.get("/books", tags=["books"])
async def get_books_route() -> list[BookDesc]:
    return await get_books()

@router.post("/books", tags=["books"])
async def create_book_route(book: BookPostForm):
    return await create_book(book)

@router.get("/books/{id}", tags=["books"])
async def get_book_route(id: int) -> Book:
    return await get_book(id)