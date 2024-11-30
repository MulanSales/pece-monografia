from transformers import RobertaModel, RobertaTokenizer
import torch
import smell_detector_api.helpers.preprocess as pp

def tranform(code_messages):

    tokens = [_tokenize(pp.clean_code_bert(code)) for code in code_messages]
    return tokens

def _tokenize(code):

    model = RobertaModel.from_pretrained("microsoft/codebert-base")
    tokenizer = RobertaTokenizer.from_pretrained("microsoft/codebert-base")

    inputs = tokenizer(code, return_tensors='pt', padding=True, truncation=True)

    with torch.no_grad():
        outputs = model(**inputs)

    return outputs.last_hidden_state[:, 0, :].squeeze().numpy()