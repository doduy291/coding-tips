import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import CancelFetching from "./features/cancel-fetching";

import PreventDuplicateObjectValue from "./features/prevent-duplicate-object-value";

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <div className="card-container">
          <Link to="/cancel-fetching" className="card">
            Cancel Fetching
          </Link>
          <Link to="/prevent-duplicate-object-value" className="card">
            Prevent Duplicate Object Value
          </Link>
        </div>

        <Routes>
          <Route path="/cancel-fetching/*" element={<CancelFetching />} />

          <Route
            path="/prevent-duplicate-object-value"
            element={<PreventDuplicateObjectValue />}
          />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
