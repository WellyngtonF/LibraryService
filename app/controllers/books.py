from fastapi import APIRouter

router = APIRouter()

@router.get("/books", tags=["books"])
async def get_books():
    return [{"id": 1, "description": "Criando microsserviços"}, {"id": 2, "description": "Microsserviços prontos para produção"}]