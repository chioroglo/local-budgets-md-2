source .venv/scripts/activate
cd src
echo "-=LEARN MODEL=-"
python main.py "DRIVER={ODBC Driver 17 for SQL Server};SERVER=localhost;DATABASE=Warehouse;UID=sa;PWD=P@ssword!;"