import './App.css';
import { Button, FormControl, TextField } from '@mui/material';
import { useState } from 'react';
import { useForm, SubmitHandler } from "react-hook-form"

type Inputs = {
  message: string
}

type Response = {
  code_smell: string,
  description: string,
  sugestion: string
}

function App() {
  const [response, setResponse] = useState<Response | undefined>(undefined);

  const {
    register,
    handleSubmit
  } = useForm<Inputs>()
  const onSubmit: SubmitHandler<Inputs> = async (data) => {

    var request = {message: data.message};

    try {
      const res = await fetch("http://127.0.0.1:8000/predict", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(request)
      });

      if (res.ok) {
        const jsonResponse = await res.json();
        setResponse(jsonResponse);
      } else {
        throw new Error('Erro na requisição');
      }
    } catch (error) {
      console.error('Erro:', error);
    }
  };


  return (
    <div className="App">
      <div className='App-container'>
        <h2 className='App-h1'>Smell Detector</h2>
        <div className='App-subcontainer'>
          <form className='App-form' onSubmit={handleSubmit(onSubmit)}>
            <FormControl className='App-form-control'>
              <TextField color='success' label="trecho de código" multiline {...register("message")} />

            </FormControl>

            <Button className='App-btn' variant='contained' type='submit' color='success'>Enviar</Button>
          </form>
        </div>
        {response && response.code_smell !== "Sem code smells" && <div className='App-csb'>
          <h3>Code Smell Encontrado</h3>
          <span><b>Nome:</b> {response?.code_smell}</span>
          <span><b>Descrição:</b> {response?.description}</span>
          <span><b>Sugestão de Correção:</b> {response?.sugestion}</span>
        </div>}  
        {response && response.code_smell === "Sem code smells" && <div className='App-csb'>
          <span>Nenhum Code Smell Encontrado</span>
        </div>} 
      </div>
        
    </div>
  );
}

export default App;
