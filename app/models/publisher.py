from pydantic import BaseModel

class Publisher(BaseModel):
    id: int
    name: str

class PublisherPostForm(BaseModel):
    name: str