import { AutenticacaoProvider } from "./Context/AutenticacaoContexto";
import { Rotas } from "./routes/Routes"
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <>
      <AutenticacaoProvider>
        <Rotas/> 
      </AutenticacaoProvider>
    </>
  )
}

export default App
