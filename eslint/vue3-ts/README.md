```js
$ npm i --save-dev eslint eslint-plugin-vue @typescript-eslint/eslint-plugin @vue/eslint-config-typescript
```

`.eslintrc.json`

```js
{
  "env": {
    "browser": true,
    "node": true
  },
  /* Extends */
  // implement the entire plugins
  "extends": [
    "eslint:recommended", // eslint
    "plugin:vue/vue3-recommended", // eslint-plugin-vue
    "@vue/eslint-config-typescript/recommended", // @vue/eslint-config-typescript
    "plugin:@typescript-eslint/recommended" // @typescript-eslint/eslint-plugin
  ],

  /* Parser */
  // Allow Eslint lint typescript code
  "parser": "vue-eslint-parser", // vue-eslint-parser
  "parserOptions": {
    "parser": "@typescript-eslint/parser" // installed with @typescript-eslint/eslint-plugin
  },

  /* Plugins */
  // Only use "plugins" with customize rules (reference: https://www.npmjs.com/package/eslint-plugin-react-hooks)
  "plugins": [],

  /* Rules */
  "rules": {
    "@typescript-eslint/ban-types": "error",
    "@typescript-eslint/no-unused-vars": "error",
    "@typescript-eslint/no-explicit-any": "error",
    "@typescript-eslint/no-empty-interface": "off",
    "@typescript-eslint/no-inferrable-types": "off"
  }
}

```
