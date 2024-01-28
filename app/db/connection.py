from sqlalchemy import create_engine, pool, Connection, text

db_name = 'library'
db_user = 'wellyngtons'
db_pass = '123456'
db_host = 'postgres-library'
db_port = '5432'

db_string = f'postgresql://{db_user}:{db_pass}@{db_host}:{db_port}/{db_name}'
# create engine without transaction mode and with pool
try:
    db = create_engine(db_string, pool_size=20, max_overflow=0, isolation_level="AUTOCOMMIT")
except Exception as e:
    print(e)

def get_connection() -> Connection:
    try:
        conn = db.connect()
        return conn
    except Exception as e:
        print(e)
        return None