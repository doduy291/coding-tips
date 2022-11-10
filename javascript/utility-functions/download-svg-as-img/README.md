```js
const downloadQRSvgAsPNG = () => {
  const svg = document.querySelector("#svg-id") as Element;
  const uriData = `data:image/svg+xml;base64,${btoa(
    new XMLSerializer().serializeToString(svg),
  )}`;

  const img = new Image();
  img.src = uriData;
  img.onload = () => {
    const canvas = document.createElement("canvas") as HTMLCanvasElement;
    canvas.width = 168;
    canvas.height = 168;
    const ctx = canvas.getContext("2d");
    ctx?.drawImage(img, 0, 0);

    // Use <a> element to download
    const a = document.createElement("a");
    const quality = 1.0;
    a.href = canvas.toDataURL("image/png", quality);
    a.download = `custom-file-name.png`;
    a.click();
    a.remove();
  };
};

```
