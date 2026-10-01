
import { Routes, Route, Link } from 'react-router'
import LoginPage from './pages/login/Login'

function App(): React.JSX.Element {


  return (
    <>
      <Routes>
        <Route path='/' element={<LoginPage />}></Route>
      </Routes>


    </>
  )
}

export default App
