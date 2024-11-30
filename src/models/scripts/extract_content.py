import json
import os

def transform(input_file, output_dir, class_prefix="GeneratedClass"):
    if not os.path.exists(output_dir):
        os.makedirs(output_dir)
        
    with open(input_file, 'r', encoding='utf-8') as infile:
        for idx, line in enumerate(infile):
            try:
                data = json.loads(line.strip())
                
                if "new_contents" in data:
                    new_contents = data["new_contents"]

                    if (data["license"] != "mit"):
                        continue
                    
                    output_file = os.path.join(output_dir, f"{class_prefix}_{idx+1}.cs")
                    
                    with open(output_file, 'w', encoding='utf-8') as outfile:
                        
                        for content_line in new_contents.splitlines():
                            clean_line = content_line.strip()

                            outfile.write(f"{clean_line}\n")
            
            except json.JSONDecodeError as e:
                print(f"An error occured while processing file: {e}")

input_file = '../raw_commits/100_selected_commits.jsonl'
output_file = '../raw_codes/'

transform(input_file, output_file)
