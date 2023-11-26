from fastapi import FastAPI
from app.controllers import books

app = FastAPI()

app.include_router(books.router)

@app.get("/")
def root():
    return {"message": "Hello World"}
