from fastapi import APIRouter, HTTPException

router = APIRouter()

mock_books = [
    {"id": 1, "description": "Criando microsserviços"},
    {"id": 2, "description": "Microsserviços prontos para produção"}
]

@router.get("/books", tags=["books"])
async def get_books():
    return mock_books

@router.get("/books/{id}", tags=["books"])
async def get_book(id: int):
    book = next((book for book in mock_books if book["id"] == id), None)
    if book is not None:
        return book
    else:
        raise HTTPException(status_code=404, detail="Book not found")
    
@router.post("/books", tags=["books"])
async def create_book(book: dict):
    mock_books.append(book)
    return book

@router.put("/books/{id}", tags=["books"])
async def update_book(id: int, book: dict):
    book = next((book for book in mock_books if book["id"] == id), None)
    if book is not None:
        book.update(book)
        return book
    else:
        raise HTTPException(status_code=404, detail="Book not found")
    
@router.delete("/books/{id}", tags=["books"])
async def delete_book(id: int):
    book = next((book for book in mock_books if book["id"] == id), None)
    if book is not None:
        mock_books.remove(book)
        return {"message": "Book deleted"}
    else:
        raise HTTPException(status_code=404, detail="Book not found")