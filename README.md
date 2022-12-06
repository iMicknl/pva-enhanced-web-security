# pva-enhanced-web-security

In this example, we host a webchat canvas on Azure Static WebApps. The Power Virtual Agents chatbot has [enhanced webchannel security](https://learn.microsoft.com/en-us/power-virtual-agents/configure-web-security) enabled and thus cannot be accessed via the default demo website and embed code. Using the secret from PVA, the secured back-end API will generate a token (valid for one user session / conversation) and pass this to the chat canvas.

This will stop other websites from being able to embed your chatbot, since they can't access your token API. You can secure your token API with an authentication provider, limit it to certain IP-addresses or only allow access in your corporate network (VNET).

## Code
[app](./app/) - frontend which loads a custom webchat canvas

[api](./api/) - C# backend API which can generate a token

## Demo
https://gray-plant-084f3ab03.2.azurestaticapps.net/

On this demo website, the token API is secured by a GitHub login. When you load the page the chat canvas won't render, but when you login the chat canvas will render. As mentioned above, you could also secure the token API in other ways like a network restriction.

## LICENSE
MIT
