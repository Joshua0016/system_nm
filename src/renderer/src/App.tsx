
import { Routes, Route, Link } from 'react-router'
import LoginPage from './pages/login/Login'
import HomePage from './pages/home/HomePage'
import CreateProduct from './modules/inventory/CreateProduct'

function App(): React.JSX.Element {


  return (
    <>
      <Routes>
        <Route path='/' element={<LoginPage />}></Route>
        <Route path='/home' element={<HomePage />}>
          <Route path='createProduct' element={<CreateProduct />}></Route>
        </Route>

      </Routes>


    </>
  )
}

export default App
