import os
import json
import re

def convert_to_jsonl(folder_path, output_file):
    """
    Converte arquivos C# verificados manualmente e com trechos selecionados
    na base de commits que será utilizada para o treinamento
    """
    try:
        csharp_files = sorted([f for f in os.listdir(folder_path) if f.endswith(".cs")], key=_extract_name_and_number)
        print(csharp_files)
        if not csharp_files:
            print("No C# files found in the specified folder.\n")
            return
        
        with open(output_file, 'w', encoding='utf-8') as jsonl_file:
            for csharp_file in csharp_files:
                file_path = os.path.join(folder_path, csharp_file)
                
                with open(file_path, 'r', encoding='utf-8') as f:
                    content = f.read()
                
                json_obj = {"new_contents": content}
                
                jsonl_file.write(json.dumps(json_obj) + '\n')
        
        print(f"Successfully converted {len(csharp_files)} files to {output_file}\n")
    
    except Exception as e:
        print(f"An error occurred: {e}\n")

def _extract_name_and_number(file_name):
    match = re.match(r'([a-zA-Z_]+)(\d+)', file_name)
    if match:
        name = match.group(1) 
        number = int(match.group(2))
        return (name, number)
    return (file_name, 0)