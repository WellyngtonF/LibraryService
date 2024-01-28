from fastapi import APIRouter, Depends, HTTPException
from app.services.author_service import *
from app.models.author import AuthorPostForm, Author

router = APIRouter()

@router.get("/authors", tags=["authors"])
async def get_authors_route() -> list[Author]:
    result = await get_authors()
    return result

@router.get("/authors/{author_id}", tags=["authors"])
async def get_author_route(author_id: int) -> Author:
    result = await get_author(author_id)
    return result

@router.post("/authors", tags=["authors"])
async def create_author_route(author: AuthorPostForm):
    return await create_author(author)