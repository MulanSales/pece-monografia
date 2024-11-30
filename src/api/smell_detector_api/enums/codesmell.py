from enum import Enum

class CodeSmell(Enum):
    NO_CODE_SMELL = 0
    INEFFICIENT_STRING_CONCATENATION = 1
    FEATURE_ENVY = 2
    LONG_PARAMETER_LIST = 3