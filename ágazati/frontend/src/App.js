import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Nav from './components/Nav';
import Home from './pages/Home';
import ListPizza from './pages/ListPizza';
import SinglePizza from './pages/SinglePizza';
import NewPizza from './pages/NewPizza';
import UpdatePizza from './pages/UpdatePizza';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Nav />

        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/pizzak' element={<ListPizza />} />
          <Route path='/pizza/:id' element={<SinglePizza />} />
          <Route path='/ujpizza' element={<NewPizza />} />
          <Route path='/pizzamodosito/:id' element={<UpdatePizza />} />
        </Routes>


      </BrowserRouter>
      <footer>@2025</footer>
    </div>
  );
}

export default App;
