from fastapi import APIRouter

from app.models.wishlist import Wishlist, WishlistPostForm
from app.services.wishlist_service import get_wishlist, create_wishlist, delete_wishlist

router = APIRouter()

@router.get("/wishlist", response_model=list[Wishlist])
async def get_wishlist():
    return await get_wishlist()

@router.get("/wishlist/{priority}", response_model=list[Wishlist])
async def get_wishlist_by_priority(priority: int):
    return await get_wishlist(priority)

@router.post("/wishlist", response_model=Wishlist)
async def post_wishlist(wishlist: WishlistPostForm):
    return await create_wishlist(wishlist)

@router.delete("/wishlist/{id}")
async def delete_wishlist(id: int):
    await delete_wishlist(id)
    return {"message": "Wishlist item deleted"}