using System.Web;

namespace ECommerceApi.Api.View;

public static class ConfirmEmailView
{
    public static string GetConfirmEmailPage(string userId, string token)
    {
        return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Confirm Your Email</title>
    <link href='https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;700&display=swap' rel='stylesheet'>
    <style>
        body {{
            font-family: 'Roboto', sans-serif;
            line-height: 1.6;
            color: #333;
            background-color: #f4f4f4;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            margin: 0;
        }}
        .container {{
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            padding: 40px;
            width: 100%;
            max-width: 400px;
            text-align: center;
        }}
        h1 {{
            color: #2c3e50;
            margin-bottom: 20px;
        }}
        button {{
            background-color: #3498db;
            color: white;
            border: none;
            padding: 12px 20px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
            transition: background-color 0.3s;
        }}
        button:hover {{
            background-color: #2980b9;
        }}
        .message {{
            margin-top: 20px;
            font-weight: bold;
        }}
        .success {{
            color: #2ecc71;
        }}
        .error {{
            color: #e74c3c;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Confirm Your Email</h1>
        <p>Click the button below to confirm your email address:</p>
        <button id='confirmButton'>Confirm Email</button>
        <p id='message' class='message'></p>
    </div>

    <script>
        document.getElementById('confirmButton').addEventListener('click', async () => {{
            const messageElement = document.getElementById('message');
            try {{
                const response = await fetch('/api/Auth/confirm-email', {{
                    method: 'POST',
                    headers: {{ 'Content-Type': 'application/json' }},
                    body: JSON.stringify({{
                        userId: '{HttpUtility.JavaScriptStringEncode(userId)}',
                        token: '{HttpUtility.JavaScriptStringEncode(token)}'
                    }})
                }});

                const result = await response.json();

                if (response.ok) {{
                    messageElement.textContent = 'Email confirmed successfully!';
                    messageElement.className = 'message success';
                    document.getElementById('confirmButton').style.display = 'none';
                }} else {{
                    messageElement.textContent = result.message || 'An error occurred. Please try again.';
                    messageElement.className = 'message error';
                }}
            }} catch (error) {{
                console.error('Error:', error);
                messageElement.textContent = 'An unexpected error occurred. Please try again later.';
                messageElement.className = 'message error';
            }}
        }});
    </script>
</body>
</html>
        ";
    }
}