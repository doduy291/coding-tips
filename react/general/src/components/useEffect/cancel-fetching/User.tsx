import React, { useEffect, useState } from "react";
import { Link, useLocation } from "react-router-dom";

// * The correct way to get API request with useEffect* //

interface User {
  name: string;
  username: string;
  email: string;
}
const User = () => {
  const [user, setUser] = useState<User>({} as User);
  const id = useLocation().pathname.split("/")[2];

  useEffect(() => {
    let unsubcribe = false;
    fetch(`https://jsonplaceholder.typicode.com/users/${id}`)
      .then((res) => res.json())
      .then((data) => {
        // Should put flag in here due to asynchronous's fetch
        console.log(unsubcribe);
        if (!unsubcribe) {
          setUser(data);
        }
      });
    return () => {
      console.log("Canceled Fetching");
      unsubcribe = true;
    };
  }, [id]);

  return (
    <div>
      <p>Name: {user?.name}</p>
      <p>Username: {user?.username}</p>
      <p>Email: {user?.email}</p>
      <Link to="/user/1">User 1</Link>
      <Link to="/user/2">User 2</Link>
      <Link to="/user/3">User 3</Link>
      <Link to="/">hOME</Link>
    </div>
  );
};

export default User;
