import { BrowserRouter, Routes, Route } from "react-router-dom";
import CancelFetchingUser from "./User";

// * The correct way to get API request with useEffect* //
const Home = () => {
  return (
    <div>
      Test
      <BrowserRouter>
        <Routes>
          <Route path="/user/:id" element={<CancelFetchingUser />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
};

export default Home;
