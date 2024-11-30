import re

def clean_code_bert(code_block):
    """
        Limpa o código C# para o preprocessamento do CodeBERT
    """
    # 1. Remover comentários de linha e bloco
    code_block = re.sub(r"\/\/.*", "", code_block)
    code_block = re.sub(r"\/\*[\s\S]*?\*\/", "", code_block)
 
    # 2. Remover sumários
    code_block = re.sub(r"<summary>.*?</summary>", "", code_block, flags=re.DOTALL)

    # 3. Padronizar caracteres especiais
    code_block = code_block.replace("\n", " <NEWLINE> ")
    code_block = code_block.replace("\t", " <TAB> ")
    code_block = code_block.replace("\ufeff", "")
    code_block = code_block.replace("\r", " <CARRIAGE> ")

    # 4. Remover diretivas de pré-processador
    code_block = re.sub(r"#if.*?#endif", "", code_block, flags=re.DOTALL)

    return code_block


def clean_code(code_block):
    """
        Limpa o código C# para o preprocessamento do Word2Vec
    """
    # 1. Remover comentários de linha e bloco
    code_block = re.sub(r"\/\/.*", "", code_block)
    code_block = re.sub(r"\/\*[\s\S]*?\*\/", "", code_block)

    # 2. Remover espaços desnecessários e normalizar quebras de linha
    code_block = re.sub(r"[ \t]+", " ", code_block)
    code_block = re.sub(r"\n\s*\n", "\n", code_block)

    # 3. Substituir trechos de código de classes com referência externa em métodos por token
    code_block = _replace_external_ref(code_block)
    
    # 4. Remover sumários
    code_block = re.sub(r"<summary>.*?</summary>", "", code_block, flags=re.DOTALL)
    
    # 5. Substituir definição de métodos por token
    code_block = re.sub(r"(public|private|protected|internal|static|virtual|override|abstract|\s)*\s*(\w+)\s+\w+\s*\(([^)]*)\)\s*\{", _replace_method_definition, code_block)
    
    # 6. Substituir invocação de métodos de objetos ou classes estaticas em tokens
    code_block = re.sub(r"\b[a-z]\w*\.\w+\(.*?\)", " <OBJECTMETHODINVOCATION> ", code_block)
    code_block = re.sub(r"\w+\[[^\]]+\]\.\w+\(.*?\)", " <OBJECTMETHODINVOCATION> ", code_block)
    code_block = re.sub(r"\b[A-Z]\w*\.\w+\(.*?\)\.\w*", " <CLASSMETHODINVOCATIONPROPERTYACCESS> ", code_block)
    code_block = re.sub(r"\b[A-Z]\w*\.\w+\(.*?\)", " <CLASSMETHODINVOCATION> ", code_block)
    code_block = re.sub(r"(<CLASSMETHODINVOCATION>\?).\w*", " <NULLABLECLASSMETHODINVOCATION>", code_block)

    # 7. Padronizar caracteres especiais
    code_block = code_block.replace("\n", " <NEWLINE> ")
    code_block = code_block.replace("\t", " <TAB> ")
    code_block = code_block.replace("\ufeff", "")
    code_block = code_block.replace("\r", " <CARRIAGE> ")

    # 8. Remover diretivas de pré-processador
    code_block = re.sub(r"#if.*?#endif", "", code_block, flags=re.DOTALL)

    # 9. Substituir valores literais por placeholders
    code_block = re.sub(r"\".*?\"", " <STRINGVALUE> ", code_block)
    code_block = re.sub(r"\b\d+(\.\d+)?\b", " <NUMBERVALUE> ", code_block)
    code_block = re.sub(r"\b\d+L\b", " <LONGVALUE> ", code_block)

    # 8. Substituir elementos como nome de atributos, classes, namespaces e classes por token
    code_block = re.sub(r"\w+\[[^\]]+\]", " <ARRAYACCESS> ", code_block)
    code_block = re.sub(r"\[[^\]]+(?:\s*)?\]", " <ATTRIBUTE> ", code_block)
    code_block = re.sub(r"\s*using\s+[a-zA-Z0-9_.]+;", " using <LIBRARY> ", code_block)
    code_block = re.sub(r"\s*namespace\s+[a-zA-Z0-9_.]+", " namespace <NAMESPACE> ", code_block)
    code_block = re.sub(r"\s*class\s+[a-zA-Z0-9_.]+", " class <CLASSNAME> ", code_block)

    # 9. Substituir execução do método por token
    code_block = re.sub(r"([a-zA-Z_][a-zA-Z0-9_]*)\s*\(([^)]*)\)\s*;", _replace_method_execution, code_block)

    # 10. Substituir acesso a propriedade de objeto de nível 1 por token  
    code_block = re.sub(r"(\w+\.\w+(\[\w+\])*(\.\w+(\[\w+\])*)*)(?!\s*\()", " <OBJECTPROPERTYACCESS> ", code_block)

    # 11. Substituir nomes de variaveis por token
    code_block = re.sub(r"\b(int|var|string|float|double|bool|char|decimal)\s+\w+", " <VARIABLENAME> ", code_block)
    
    # 12. Substituir nomes de privadas variaveis por token
    code_block = re.sub(r"\s+[a-zA-Z_]\w*\.", " <PRIVATEVARIABLE> ", code_block)

    # 13. Substituir instanciação de classes por token
    code_block = re.sub(r"new\s((?!int|bool|float|double|char|long|string|decimal|byte|short|object|sbyte|ushort|uint|ulong)\w+)", " <CLASSINIT> ", code_block)
    
    # 14. Substituir nomes de variáveis de tipos nativos, primitivos e literais
    code_block = re.sub(r'(?<=\s|\()(?!(int|var|string|float|double|bool|char|long|short|byte|decimal|void|public|private|protected|internal|static|readonly|new|abstract|sealed|enum|class|struct|interface|delegate|if|else|for|foreach|while|do|switch|case|try|catch|finally|throw|return|using|namespace|this|typeof|base|List|IntPtr|true|false|StringBuilder|extern|UInt32|in|get|set|async|await|Task|DateTime)\s?)(\w+)(?=\s*[+\-*/%&|;^<>]=?|[\)\s])', " <VARIABLENAME> ", code_block)
    
    return code_block

def _replace_method_execution(match):
    parameters = match.group(2).strip()
    if parameters:
        types = ["<PARAM>" for _ in parameters.split(",")]
        token_params = f"{', '.join(types)}"
    else:
        token_params = ""
    return f"<METHOD_EXEC: ({token_params})>;"

def _replace_method_definition(match):
    modifiers = match.group(1) or ""
    return_type = match.group(2)
    parameters = match.group(3).strip()
    
    if parameters:
        # Capturar os tipos dos parâmetros
        parameters_types = [param.split()[0] for param in parameters.split(",")]
        parameters_types = f"{', '.join(parameters_types)}"
    else:
        parameters_types = ""

    placeholder = f" <METHOD_DEF:LENGHT_GTT:{len(parameters_types)>5}>" + " {"
    return re.sub(r"\b(TYPE:)\w*", "<CLASSOBJECT>", placeholder)

def _replace_external_ref(code):
    method_pattern = r"\b(?:public|private|protected)?\s*(?:void|[A-Z]\w*)\s+\w+\((.*?)\)\s*{"
    param_pattern = r"\b([A-Z]\w*)\s+(\w+)\b"

    matches = re.finditer(method_pattern, code)
    external_vars = set()

    for match in matches:
        params = match.group(1)
        param_matches = re.findall(param_pattern, params)
        for param in param_matches:
            if param[0][0].isupper():
                external_vars.add(param[1])

    for var in external_vars:
        code = re.sub(rf"\b{var}\b", " <EXTERNALCLASSREF>", code)

    return code