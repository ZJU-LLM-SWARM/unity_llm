from cache import DiskCache
import openai

openai.api_base = "https://api.gptapi.us/v1"
openai.api_key = "API_KEY"

def safe_exec(gpt_respond,gvars=None, lvars=None):#gvars是全局变量 lvars是局部变量
    banned_phrases = ['import', '__']#违禁词 不能超出生成范围 实际上prompt中也有限制

    code_str = gpt_respond.replace('```', '').replace('python', '').strip()
    for phrase in banned_phrases:
        assert phrase not in code_str
  
    if gvars is None:
        gvars = {}
    if lvars is None:
        lvars = {}
    empty_fn = lambda *args, **kwargs: None
    custom_gvars ={**gvars,**{'exec': empty_fn, 'eval': empty_fn}}
    try:
        exec(code_str, custom_gvars, lvars)
    except Exception as e:
        print(f'Error executing code:\n{code_str}')
        raise e
    
    
msg = [{"role": "user", "content":"give me a simple example of a python program of quicksort, only python code, no explanation, no comments, no imports, no libraries"}]
ret = openai.ChatCompletion.create(model = "gpt-3.5-turbo",messages = msg)['choices'][0]['message']['content']

cache = DiskCache('cache',True)
content = ret.replace('```', '').replace('python', '').strip()
cache._save_to_disk('gpt-3.5-turbo-processed',content)

cache._load_cache()
gptcache = cache['gpt-3.5-turbo-processed']

safe_exec(gptcache)

