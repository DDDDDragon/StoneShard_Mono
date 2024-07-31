#include<bits/stdc++.h>

using namespace std;

int main()
{
    int a;

    cin >> a;
    string num = to_string(a);
    
    int ind = num.length() / 2, len = num.length();

    while(mid > 0)
    {
        if(num[ind] < num[len - ind - 1])
        {
            if(ind == len - ind - 1)
            {
                num[ind]--;
                
            }
        }
    }
    
}