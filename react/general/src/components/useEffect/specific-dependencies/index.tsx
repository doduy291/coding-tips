import React, { useEffect, useState } from "react";

// * Use useEffect with specific dependencies* //
const useEffectCase1 = () => {
  const [field, setField] = useState("");
  const [state, setState] = useState({
    name: "",
    selected: false,
  });

  useEffect(() => {
    console.log("The state has changed, useEffect runs~");
  }, [state.name, state.selected]); // use specific key to prevent re-rendering when to get duplicated value

  const handleAdd = () => {
    setState((prev) => ({ ...prev, name: field }));
  };
  const handleSelect = () => {
    setState((prev) => ({ ...prev, selected: true }));
  };

  return (
    <div>
      <input type="text" onChange={(e) => setField(e.target.value)} />
      <button onClick={handleAdd}>Save name</button>
      <button onClick={handleSelect}>Select</button>
      {`{name: ${state.name}, selected: ${state.selected}}`}
    </div>
  );
};

export default useEffectCase1;
