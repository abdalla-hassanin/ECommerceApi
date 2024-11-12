using System.Web;

namespace ECommerceApi.Api.View;

public static class ResetPasswordView
{
    public static string GetResetPasswordPage(string email, string token)
    {
        return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Reset Your Password</title>
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
        }}
        h1 {{
            color: #2c3e50;
            text-align: center;
            margin-bottom: 30px;
        }}
        .form-group {{
            margin-bottom: 20px;
        }}
        label {{
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }}
        .input-group {{
            position: relative;
        }}
        input {{
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 16px;
        }}
        .toggle-password {{
            position: absolute;
            right: 10px;
            top: 50%;
            transform: translateY(-50%);
            cursor: pointer;
            user-select: none;
        }}
        button {{
            background-color: #3498db;
            color: white;
            border: none;
            padding: 12px 20px;
            border-radius: 4px;
            cursor: pointer;
            width: 100%;
            font-size: 16px;
            transition: background-color 0.3s;
        }}
        button:hover {{
            background-color: #2980b9;
        }}
        .error {{
            color: #e74c3c;
            margin-top: 20px;
        }}
        .success {{
            color: #2ecc71;
            margin-top: 20px;
        }}
        .hidden {{
            display: none;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Reset Your Password</h1>
        <form id='resetForm'>
            <div class='form-group'>
                <label for='password'>New Password:</label>
                <div class='input-group'>
                    <input type='password' id='password' required minlength='8'>
                    <span class='toggle-password' onclick='togglePassword(""password"")'>👁️</span>
                </div>
            </div>
            <div class='form-group'>
                <label for='confirmPassword'>Confirm Password:</label>
                <div class='input-group'>
                    <input type='password' id='confirmPassword' required minlength='8'>
                    <span class='toggle-password' onclick='togglePassword(""confirmPassword"")'>👁️</span>
                </div>
            </div>
            <button type='submit'>Reset Password</button>
        </form>
        <p id='message' class='hidden'></p>
    </div>

    <script>
        function togglePassword(inputId) {{
            const input = document.getElementById(inputId);
            input.type = input.type === 'password' ? 'text' : 'password';
        }}

        document.getElementById('resetForm').addEventListener('submit', async (e) => {{
            e.preventDefault();
            const password = document.getElementById('password').value;
            const confirmPassword = document.getElementById('confirmPassword').value;
            const messageElement = document.getElementById('message');

            messageElement.className = 'hidden';

            if (password !== confirmPassword) {{
                messageElement.textContent = 'Passwords do not match.';
                messageElement.className = 'error';
                return;
            }}

            try {{
                const response = await fetch('/api/Auth/reset-password', {{
                    method: 'POST',
                    headers: {{ 'Content-Type': 'application/json' }},
                    body: JSON.stringify({{
                        email: '{HttpUtility.JavaScriptStringEncode(email)}',
                        token: '{HttpUtility.JavaScriptStringEncode(token)}',
                        newPassword: password
                    }})
                }});

                const result = await response.json();

                if (response.ok) {{
                    messageElement.textContent = 'Password reset successful. You can now log in with your new password.';
                    messageElement.className = 'success';
                    document.getElementById('resetForm').style.display = 'none';
                }} else {{
                    messageElement.textContent = result.message || 'An error occurred. Please try again.';
                    messageElement.className = 'error';
                }}
            }} catch (error) {{
                console.error('Error:', error);
                messageElement.textContent = 'An unexpected error occurred. Please try again later.';
                messageElement.className = 'error';
            }}
        }});
    </script>
</body>
</html>
    ";
    }
}