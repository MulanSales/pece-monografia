from fastapi import FastAPI  
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel
from smell_detector_api.helpers import tokenizer, preprocess, embeddings
from smell_detector_api.enums.codesmell import CodeSmell
import joblib
import gensim
import json

model = joblib.load('./smell_detector_api/models/svm_cbb.pkl')
scaler = joblib.load("./smell_detector_api/models/scaler.pkl")

app = FastAPI()   

origins = [
    "http://localhost",
    "http://localhost:3000",
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

class CommitInput(BaseModel):
  message: str

@app.post("/predict/") 
async def predict(input: CommitInput):     

  message = json.dumps(input.message)

  prediction = _predict_CB(message)

  response = {
    "code_smell": "Sem code smells",
    "description": "Não foram encontrados code smells no trecho",
    "sugestion": "Não há sugestões"
  }

  print(prediction[0])
  if (prediction[0] == 1):
    response = {
      "code_smell": "Concatenação de String Ineficiente {Ineficient String Concatenation}",
      "description": """Ocorre em C# quando várias concatenações de strings são realizadas 
      de maneira ineficiente, normalmente utilizando o operador + ou += em um loop ou em situações 
      de muitas operações de concatenação. Isso pode impactar negativamente o desempenho porque strings
      em C# são imutáveis, e toda vez que você concatena strings dessa forma, uma nova string é criada na memória, 
      o que pode gerar overhead significativo em termos de alocação e coleta de lixo (Garbage Collection).""",
      "sugestion": """Em C# existe uma classe, baseado no padrão de design Builder, 
      que lida melhor com a criação de strings que precisam de concatenção, 
      mantendo a mesma referência, ou seja, se a necessidade de novas alocação, 
      que no caso é a StringBuilder da System.Text"""
    }
  elif (prediction[0] == 2):
    response = {
      "code_smell": "Inveja de Recursos {Feature Envy}",
      "description": """Feature envy ocorre quando um método parece mais interessado nos dados de outra classe que da sua própria. Isso pode indicar a necessidade de refatorar para melhorar o encapsulamento.""",
      "sugestion": """Internalizar lógica para a classe que contém a propriedades de interesse"""
    }
  elif (prediction[0] == 3):
    response = {
  "code_smell": "Lista Longa de Paramêtros {Long Parameter List}",
      "description": """Métodos que aceitam muitos parâmetros, tornando o código difícil de ler e usar.""",
      "sugestion": """Uma maneira de refatorar esse tipo de code smell é agrupando os parâmetros relacionados em uma classe"""
    }

  return response

def _predict_CB(message):
    commit_embeddings = tokenizer.tranform([message])

    e_normalized = scaler.transform(commit_embeddings)
    
    prediction = model.predict(e_normalized)

    return prediction

def _predict_W2Vec(message):
  x_cleaned = preprocess.clean_code(message)
  
  loaded_model = gensim.models.Word2Vec.load("./smell_detector_api/models/word2vec_model.model")

  commit_embeddings = embeddings.textToVector(loaded_model.wv, message, 300, 300)

  print(commit_embeddings)
  prediction = model.predict(commit_embeddings.reshape(1, -1))

  return prediction