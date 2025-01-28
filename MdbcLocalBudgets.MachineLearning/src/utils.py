class ColorfulPrint:
    OKGREEN = "\033[92m"
    ENDC = '\033[0m'

    @staticmethod
    def print_success(phrase: str):
        print(ColorfulPrint.OKGREEN + phrase + ColorfulPrint.ENDC)
