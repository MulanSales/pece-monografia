import numpy as np

def textToVector(model, text, vector_size, seq_len):
    """
        (1) Divide o texto em palavras usando split()
        (2) Determina a quantidade de palavras a serem processadas
        (3) Limita caso ultrapasse o tamanho definido por seq_len
        (4) Converte tokens em vetores
        (5) Preenchimento de tamanho fixo, dado o tamanho do vector usado no modelo
        (6) transforma o vetor para uma representação unidimensional
    """
    tokens = text.split()

    tokens_len = len(tokens)

    len_v = tokens_len - 1

    if (tokens_len >= seq_len):
        len_v = seq_len - 1
        
    vector = []
    for tok in tokens[:len_v]:
        try:
            vector.append(model[tok])
        except Exception as E:
            pass
    
    last_pieces = seq_len - len(vector)
    for i in range(last_pieces):
        vector.append(np.zeros(vector_size,))
    
    return np.asarray(vector).flatten()