import { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";

// * The correct way to get API request with useEffect* //

interface User {
  name: string;
  username: string;
  email: string;
}
const AbortControllerMethod = () => {
  const [user, setUser] = useState<User>({} as User);
  const id = useLocation().pathname.split("/")[3];

  /* Important */
  const controller = new AbortController();
  const signal = controller.signal;

  useEffect(() => {
    fetch(`https://jsonplaceholder.typicode.com/users/${id}`, {
      signal: signal,
    })
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setUser(data);
      });
    return () => {
      console.log("Canceled Fetching");
      controller.abort();
    };
  }, [id]);

  return (
    <div>
      <p>Name: {user?.name}</p>
      <p>Username: {user?.username}</p>
      <p>Email: {user?.email}</p>
      <Link to="/cancel-fetching/abort-controller/1">User 1</Link>
      <Link to="/cancel-fetching/abort-controller/2">User 2</Link>
      <Link to="/cancel-fetching/abort-controller/3">User 3</Link>
      <a href="/cancel-fetching">Test</a>
    </div>
  );
};

export default AbortControllerMethod;
