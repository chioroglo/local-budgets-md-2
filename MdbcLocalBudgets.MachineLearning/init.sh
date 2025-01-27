if [ ! -d ".venv" ]; then
    # If not, create a virtual environment
    python -m venv .venv
fi
echo "activating venv"
source .venv/scripts/activate
echo "venv activated"
python -m pip install --upgrade pip
pip install -r requirements.txt