from fastapi import FastAPI
import uvicorn
from app.routes import books, genres, authors, publishers, wishlist

app = FastAPI()

app.include_router(books.router)
app.include_router(genres.router)
app.include_router(authors.router)
app.include_router(publishers.router)
app.include_router(wishlist.router)

@app.get("/")
def root():
    return {"message": "Hello World"}

if __name__ == "__main__":
    uvicorn.run(app, host="localhost", port=8000)