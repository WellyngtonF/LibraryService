# Funções esperadas:

* Armazenar lista de livros da biblioteca

* Permitir cadastro de novos livros

* Permitir alteração de livros cadastrados

* Permitir exclusão de livros cadastrados

* Permitir consulta de livros cadastrados

* Permitir consulta de livros por autor

* Permitir consulta de livros por título

* Permitir consulta de livros por editora

* Permitir consulta de livros por ano de publicação

* Permitir consulta de livros por gênero

* Armazenar informação de livros lidos

* Armazenar informação de lista de desejos

* Encaminhar lista de desejos para o serviço de wishlist, responsável pelo scrapping de lojas online

# Endpoints

* /books - GET - Retorna todos os livros cadastrados

* /books - POST - Cadastra um novo livro

* /books/:id - PUT - Atualiza um livro

* /books/:id - DELETE - Deleta um livro

* /books/:id - GET - Retorna um livro específico

* /books/author/:author - GET - Retorna todos os livros de um autor

* /books/title/:title - GET - Retorna todos os livros de um título (regex)

* /books/publisher/:publisher - GET - Retorna todos os livros de uma editora

* /books/year/:year - GET - Retorna todos os livros de um ano

* /books/genre/:genre - GET - Retorna todos os livros de um gênero

* /books/read/:id - PUT - Atualiza o status de leitura de um livro

* /wishlist - GET - Retorna todos os livros da wishlist

* /wishlist - POST - Adiciona um livro à wishlist

* /wishlist/:id - DELETE - Deleta um livro da wishlist

* /wishlist/:id - GET - Retorna um livro específico da wishlist

* /wishlist/:id - PUT - Atualiza um livro da wishlist