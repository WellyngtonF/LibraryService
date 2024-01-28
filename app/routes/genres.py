from fastapi import APIRouter, HTTPException, File, UploadFile
from app.services.genre_services import *
from app.models.genre import GenrePostForm

router = APIRouter()

@router.get("/genres", tags=["genres"])
async def get_books_route():
    return get_genres()

@router.post("/genres", tags=["genres"])
async def create_book_route(genre: GenrePostForm):
    return await create_genre(genre)