from pydantic import BaseModel
from datetime import date
from typing import List

class GenrePostForm(BaseModel):
    name: str