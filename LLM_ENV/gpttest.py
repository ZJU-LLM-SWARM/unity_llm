from langchain.llms import OpenAI
from langchain.prompts import ChatPromptTemplate, HumanMessagePromptTemplate, PromptTemplate
from langchain.output_parsers import StructuredOutputParser, ResponseSchema
import os

openai_api_key = "sk-7tZmGaqA6yEkcuAPA8C4249a86F449De82D1224388A7253c"
print(openai_api_key)

llm = OpenAI(model_name="gpt-4", openai_api_key=openai_api_key)

# 定义结构
response_schemas = [
    ResponseSchema(name="drone1", description="输出格式为[a,b,c,d]，前进时a为1，后退时为-1，否则为0；向左平移时b为1，向右平移时b为-1，否则为0;上升时c为1，下降时c为-1，否则为0;逆时针旋转d为1，顺时针旋转时d为-1，否则为0"),
    ResponseSchema(name="drone2", description="输出格式为[a,b,c,d]，前进时a为1，后退时为-1，否则为0；向左平移时b为1，向右平移时b为-1，否则为0;上升时c为1，下降时c为-1，否则为0;逆时针旋转d为1，顺时针旋转时d为-1，否则为0"),
    ResponseSchema(name="car1", description="输出格式为[a,b,c]，前进时a为1，后退时为-1，否则为0；左转b为1，右转b为-1，否则为0；刹车时c为1，否则为0"),
    ResponseSchema(name="car2", description="输出格式为[a,b,c]，前进时a为1，后退时为-1，否则为0；左转b为1，右转b为-1，否则为0；刹车时c为1，否则为0"),

    # ResponseSchema(name="car1", description="只输出以包含3个元素的数组，第一个元素的取值为1表示为前进，为-1表示后退，否则为0；第二个元素的取值为1表示左转，取值为-1表示右转，否则为0，第三个元素为1时表示刹车，否则为0"),
    # ResponseSchema(name="car2", description="只输出以包含3个元素的数组，第一个元素的取值为1表示为前进，为-1表示后退，否则为0；第二个元素的取值为1表示左转，取值为-1表示右转，否则为0，第三个元素为1时表示刹车，否则为0"),
    # ResponseSchema(name="drone1", description="只输出以包含4个元素的数组，注意区分平移和旋转，第一个元素为1表示向前平移，-1表示向后平移，否则为0；第二个元素取值为1表示向左平移，-1表示向右平移，否则为0；第三个元素取值为1表示向上平移，-1表示向下平移，否则为0；第四个元素为1表示向左旋转，-1表示向右旋转，否则为0"),
    # ResponseSchema(name="drone2", description="只输出以包含4个元素的数组，注意区分平移和旋转，第一个元素为1表示前进，-1表示后退，否则为0；第二个元素取值为1表示向左平移，-1表示向右平移，否则为0；第三个元素取值为1表示向上平移，-1表示向下平移，否则为0；第四个元素为1表示向左旋转，-1表示向右旋转，否则为0")
]
# 解析输出结构
output_parser = StructuredOutputParser.from_response_schemas(response_schemas)
format_instructions = output_parser.get_format_instructions()
# print('格式化的提示模板', format_instructions)

template = """
根据用户内容,提取出动作状态
{format_instructions}
% 用户输入:
{value}
"""
prompt = PromptTemplate(
    input_variables=["value"],
    partial_variables={"format_instructions": format_instructions},
    template=template
)
final_prompt = prompt.format(value="第一辆小车前进并右转，第二辆小车后退并右转,第一个无人机向左前方平移,第二个无人机顺时针旋转并下降")

# print("输入内容：:", final_prompt)
result_str = llm(final_prompt)
print("输出内容：", result_str)
with open('my_file.txt', 'w', encoding='utf-8') as f:
    f.write(result_str)

# # 打开并读取文件
# with open('my_file.txt', 'r', encoding='utf-8') as f:
#     content = f.readlines()

# # 提取特定内容后面的字符串
# substring = ["drone1","drone2","car1","car2"]
# text = []
# for x in substring:
#     for line in content:
#         if x in line:
#             parts = line.split(x)
#             if len(parts) > 1:
#                 text.append(parts[1].split('[')[1].split(']')[0]) 
#                 # print(text)  # parts[1] 是指定字符串后面的内容
#             else:
#                 print("指定字符串后面没有内容.")
# print(text)