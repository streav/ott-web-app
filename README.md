# Streav OTT Web App
[![BuiltWithDot.Net shield](https://builtwithdot.net/project/5740/ott-web-app-aspnet-core/badge)](https://builtwithdot.net/project/5740/ott-web-app-aspnet-core)

This demo OTT (Over-The-Top) web application showcases the capabilities of [Streav](https://streav.com).
Built using .NET 8, Nuxt 3, Vue 3, UnoCss, video.js, and powered by Streav API.

**Live Preview:** https://demo-ott.streav.com

## Screenshots

![1](https://github.com/streav/ott-web-app/assets/37005069/f9533c9c-dfa4-4983-ace2-f289ff5c2c3d)

![2](https://github.com/streav/ott-web-app/assets/37005069/15347d59-4b06-4629-ab3a-4ffc2628099f)

![3](https://github.com/streav/ott-web-app/assets/37005069/c2c6f417-8a45-4082-a0d2-a775061116ab)

![4](https://github.com/streav/ott-web-app/assets/37005069/687d2cd5-0881-47c3-8283-a6bca88b5ebb)

![5](https://github.com/streav/ott-web-app/assets/37005069/c9169576-3314-4bbe-b101-f4ce39d31abd)

## Setup

Before running:

- Create an OAuth application. Refer to the [API Reference](https://streav.com/api-reference/introduction)
  for more information.
- Rename the `appsettings.json.example` file to `appsettings.json`.
- Replace `"YOUR_CLIENT_ID_HERE"` and `"YOUR_CLIENT_SECRET_HERE"` with the Client ID and Client Secret.
    ```json
    "Streav": {
      "ApiBaseUrl": "http://localhost:5000/",
      "ClientId": "YOUR_CLIENT_ID_HERE",
      "ClientSecret": "YOUR_CLIENT_SECRET_HERE",
      "BundleId": 1
    }
    ```

With the configuration in place, you're ready to run the app.

## Credits

Based on [nuxt/movies](https://github.com/nuxt/movies).
