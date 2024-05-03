import os
import pandas as pd
from sqlalchemy import create_engine
from dotenv import load_dotenv

load_dotenv()
print(os.getenv("DATABASE_URL"))

engine = create_engine(database_url)

conn = engine.connect()

df = pd.read_sql("SELECT Reason FROM Appointments", conn)

print(df.head())

conn.close()