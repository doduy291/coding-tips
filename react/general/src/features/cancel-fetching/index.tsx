import { Routes, Route, Link } from "react-router-dom";
import SubscriptionMethod from "./Subscription";
import AbortControllerMethod from "./AbortController";

const CancelFetching = () => {
  return (
    <>
      <div className="card-container">
        <Link className="card-mini" to="/cancel-fetching/subscription">
          Use Subscription
        </Link>
        <Link className="card-mini" to="/cancel-fetching/abort-controller">
          Use Abort Controller API
        </Link>
      </div>

      <Routes>
        <Route path="/subscription" element={<SubscriptionMethod />} />
        <Route path="/subscription/:id" element={<SubscriptionMethod />} />
        <Route path="/abort-controller" element={<AbortControllerMethod />} />
        <Route
          path="/abort-controller/:id"
          element={<AbortControllerMethod />}
        />
      </Routes>
    </>
  );
};

export default CancelFetching;
