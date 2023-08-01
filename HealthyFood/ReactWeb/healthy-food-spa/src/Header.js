import { Link } from 'react-router-dom'


function Header() {
  return (
    <div>
      <Link to={'/'}>home</Link>
      <Link to={'/games'}>games</Link>
      <Link to={'/login'}>Login</Link>
      <Link to={'/biologicallyActiveAdditives'}>BiologicallyActiveAdditives</Link>
    </div>
  );
}

export default Header;
