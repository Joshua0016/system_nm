
import { Routes, Route, Link } from 'react-router'
import LoginPage from './pages/login/Login'
import HomePage from './pages/home/HomePage'
import CreateProduct from './modules/inventory/CreateProduct'
import CreateSuppliers from './modules/inventory/CreateSuppliers'
import CreatePurchases from './modules/inventory/CreatePurchases'
function App(): React.JSX.Element {


  return (
    <>
      <Routes>
        <Route path='/' element={<LoginPage />}></Route>
        <Route path='/home' element={<HomePage />}>
          <Route path='createProduct' element={<CreateProduct />}></Route>
          <Route path='createSuppliers' element={<CreateSuppliers />}></Route>
          <Route path='createPurchases' element={<CreatePurchases />}></Route>
        </Route>

      </Routes>


    </>
  )
}

export default App
