```js
$ npm i --save-dev eslint eslint-config-next eslint-plugin-import eslint-plugin-jsx-a11y eslint-plugin-react eslint-plugin-react-hooks @typescript-eslint/eslint-plugin
```

`.eslintrc.json`

```js
{
  /* Enviroment */
  // https://eslint.org/docs/latest/user-guide/configuring/language-options#specifying-environments
  "env": {
    "browser": true,
    "node": true
  },
  /* Extends */
  // implement the entire plugins
  "extends": [
    "next/core-web-vitals",
    "eslint:recommended", // eslint
    "plugin:react/recommended", // eslint-plugin-react
    "plugin:react-hooks/recommended", // eslint-plugin-react-hooks
    "plugin:@typescript-eslint/recommended", // @typescript-eslint/eslint-plugin
    "plugin:@next/next/recommended", // eslint-config-next
    "plugin:import/recommended" // eslint-plugin-import
  ],

  /* Parser */
  // Allow Eslint lint typescript code
  "parser": "@typescript-eslint/parser", // installed with @typescript-eslint/eslint-plugin

  /* Plugins */
  // Only use "plugins" with customize rules (reference: https://www.npmjs.com/package/eslint-plugin-react-hooks)
  "plugins": [],

  /* Rules */
  "rules": {
    "react/react-in-jsx-scope": "off",
    "react/prop-types": "off",
    "react-hooks/exhaustive-deps": "error",
    "@typescript-eslint/ban-types": "error",
    "@typescript-eslint/no-unused-vars": "error",
    "@typescript-eslint/no-explicit-any": "error",
    "@typescript-eslint/no-empty-interface": "off",
    "@typescript-eslint/no-inferrable-types": "off",
    "@next/next/no-img-element": "off"
  }
}

```
