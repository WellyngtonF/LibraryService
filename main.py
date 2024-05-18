from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
import uvicorn
from app.routes import books, genres, authors, publishers, wishlist

app = FastAPI()

origins = [
    "http://localhost:80",  # Adjust the port number if your frontend runs on a different port
    "http://127.0.0.1:80"  # Consider adding both localhost and 127.0.0.1
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"]
)

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