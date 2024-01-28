from fastapi import APIRouter, HTTPException

from app.models.publisher import PublisherPostForm, Publisher
from app.services.publisher_service import get_publishers, create_publisher

router = APIRouter()

@router.get("/publishers", response_model=list[Publisher])
async def get_publishers_route():
    return await get_publishers()

@router.post("/publishers", response_model=Publisher)
async def post_publishers_route(publisher: PublisherPostForm):
    return await create_publisher(publisher)