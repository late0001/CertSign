import os,time
from selenium import webdriver
from mail_spider import Spider as mailSpider
from selenium.webdriver.support.select import Select
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait # available since 2.4.0
from selenium.webdriver.support import expected_conditions as EC # available since 2.26.0
from selenium.webdriver.common.action_chains import ActionChains
from selenium.common.exceptions import NoSuchElementException
from selenium.common.exceptions import TimeoutException as SeleTimeoutException
from configparser import ConfigParser
 
class Spider:
    def __init__(self):     
        cfg = ConfigParser()
        cfg.read('config.ini')
        self.certsign_username = cfg.get('certsign', 'username')
        self.certsign_password = cfg.get('certsign', 'password')
        self.iflow_username = cfg.get('iflow', 'username')
        self.iflow_password = cfg.get('iflow', 'password')
        self.coeff11n = cfg.get('iflow', '11n')
        self.coeff11ac = cfg.get('iflow', '11ac')
        self.coeff11ax = cfg.get('iflow', '11ax')
        #current_path = os.path.dirname(os.path.realpath(__file__))
        current_path = os.path.abspath('.')
        driver_path=current_path + r"\chromedriver.exe"
        #self.driver=webdriver.Chrome(executable_path=driver_path)
        print("driver_path=", driver_path)
        options = webdriver.ChromeOptions()
        prefs = {'profile.default_content_settings.popups': 0,
            'download.default_directory': 'd:\\'}
        options.add_experimental_option('prefs', prefs)
        self.driver = webdriver.Chrome(executable_path=driver_path, chrome_options=options)

    def baidu(self):
        driver = self.driver
        #driver.manage().window().minimize();  
        driver.get("https://www.baidu.com")
        time.sleep(3)
        #print(driver.page_source)
        # 查找输入框
        my_input = driver.find_element_by_id('kw')
        
        # 在输入框输入文字
        my_input.send_keys('j8')
        time.sleep(3)
        
        # 搜索按钮
        button = driver.find_element_by_id('su')
        # 点击按钮
        button.click()
        time.sleep(3)

        driver.quit()
    def signFiles(self, filename):
        driver = self.driver
        driver.get("https://certsign.realtek.com/Login.jsp")
        login_username = driver.find_element_by_xpath("//*[@id=\"login_username\"]")
        login_password = driver.find_element_by_xpath("//*[@id=\"login_password\"]")
        btnLogin = driver.find_element_by_xpath("//*[@id=\"btnLogin\"]")
        login_username.send_keys(self.certsign_username)
        login_password.send_keys(self.certsign_password)
        btnLogin.click()
        time.sleep(3)
        driver.find_element_by_xpath("//*[@id=\"getOTP\"]").click()
        mailspr = mailSpider()
        optcode = mailspr.dl_mail()
        time.sleep(3)
        if optcode is not None:
            print(" ============== (*-*) ==================")
            driver.find_element_by_xpath("//*[@id=\"otp1\"]").send_keys(optcode)
            driver.find_element_by_xpath("//*[@id=\"validOTP\"]").click()
            time.sleep(2)
            #driver.find_element_by_xpath("//span[@class='buttonText'][contains(.,'Choose file')]").click()
            driver.find_element_by_xpath("//input[@type='file']").send_keys(filename)
            #driver.find_element_by_xpath("//input[@type='file']").send_keys(r"D:\xv\MP_branch\8822c\8822c\RTLWlanU_WindowsDriver_\Win7.zip")
            i = 0
            while True:
                try:
                    driver.find_element_by_xpath("//button[@type='button'][contains(.,'上傳檔案')]").click()
                    time.sleep(6)
                    i=1
                except SeleTimeoutException as e:
                    print("raise exception 上傳檔案 time out")
                finally:
                    if i > 0:
                        break
            
            s = driver.find_element_by_xpath("//select[contains(@name,'shaType')]")
            Select(s).select_by_value('A6419491-A9A6-48FE-AB09-6AF86954FBEC') #EVSign  SHA256演算法
            '''
            8B4F20EC-DB9A-49FB-9722-7A4E3F9EE077 EVSign SHA1演算法
            D03E8893-CBC6-4567-8781-430A30FA6CC3 EVSign SHA256演算法(Append)
            D75001C2-141D-42E9-9CED-41A62A8FD0E2 EVSign SHA1演算法 &amp; SHA256演算法
            '''
            driver.find_element_by_xpath("//button[@type='button'][contains(.,'簽署憑證')]").click()
            time.sleep(3)
            for i in range(0, 10):
                try:
                    driver.find_element_by_xpath("//strong[contains(.,'已完成簽署!')]")
                    break;
                except  NoElementFoundException:
                    print("未签署完成，3秒后重试 =============>")
                    time.sleep(3)
                    
            element = driver.find_element_by_xpath("//button[@type='button'][contains(.,'Download')]")
            element = WebDriverWait(self.driver,10).until(EC.visibility_of_element_located((By.XPATH, "//button[@type='button'][contains(.,'Download')]")))
            ActionChains(self.driver).move_to_element(element).click().perform()
             
    def odering(self): 
            
        driver = self.driver
        driver.get("https://iflow.realtek.com.cn/RTData/CyberStar/portal.nsf")
        login_username = driver.find_element_by_xpath("//input[@id='textUserId']")
        btnLogin = driver.find_element_by_xpath("//button[contains(@id,'buttonLogin')]")
        login_username.send_keys(self.iflow_username)
        btnLogin.click()
        login_password = driver.find_element_by_xpath("//input[contains(@id,'textPassword')]")
        login_password.send_keys(self.iflow_password)
        btnLogin.click()
        time.sleep(3)
        #driver.switch_to.window(driver.window_handles[-1])
        #sreach_window=driver.current_window_handle
        #driver.switch_to.window(driver.window_handles[-1])
        #driver.get("https://iflow.realtek.com.cn/RTData/CyberStar/portal.nsf")
        top = driver.find_element_by_name('left')
        self.driver.switch_to.frame(top)
        a_fillform =driver.find_element_by_xpath("//a[@href='/RTData/CyberStar/portal.nsf/FmWriteForm02?OpenForm']")
        a_fillform.click()
        time.sleep(3)
        # Meal subsidy application
        a_fillFoodBill =driver.find_element_by_xpath("//font[@size='3'][contains(.,'餐费补助申请单')]")
        a_fillFoodBill.click()
        driver.switch_to.window(driver.window_handles[-1])
        s = driver.find_element_by_xpath("//select[contains(@id,'MenuDetails')]")
        Select(s).select_by_value('一品佳（金证店）自选/20元') 
        btn_addgroup =driver.find_element_by_xpath("//input[contains(@value,'添加群组')]")
        btn_addgroup.click()
        time.sleep(1)
        driver.switch_to.window(driver.window_handles[-1])
        s_grp = driver.find_element_by_xpath("//select[contains(@name,'sRdep')]") #下一项依然是//select[@name='sRdep']
        Select(s_grp).select_by_value('64023 [CN 11n NIC]')
        sRdepX = driver.find_element_by_xpath("//input[@id='sRdepX']")  #下一项依然是//input[@id='sRdepX']
        sRdepX.clear()
        sRdepX.send_keys(self.coeff11n)
        sOk = driver.find_element_by_xpath("//a[@href='javascript:saveRow(0)'][contains(.,'确定')]")
        sOk.click()
        s_grp = driver.find_element_by_xpath("//select[contains(@name,'sRdep')]") #下一项依然是//select[@name='sRdep']
        Select(s_grp).select_by_value('64025 [CN 11ac NIC]')
        sRdepX = driver.find_element_by_xpath("//input[@id='sRdepX']")  #下一项依然是//input[@id='sRdepX']
        sRdepX.clear()
        sRdepX.send_keys(self.coeff11ac)
        sOk = driver.find_element_by_xpath("//a[@href='javascript:saveRow(0)'][contains(.,'确定')]")
        sOk.click()
        s_grp = driver.find_element_by_xpath("//select[contains(@name,'sRdep')]") #下一项依然是//select[@name='sRdep']
        Select(s_grp).select_by_value('64026 [CN 11ax NIC]')
        sRdepX = driver.find_element_by_xpath("//input[@id='sRdepX']")  #下一项依然是//input[@id='sRdepX']
        sRdepX.clear()
        sRdepX.send_keys(self.coeff11ax)
        sOk = driver.find_element_by_xpath("//a[@href='javascript:saveRow(0)'][contains(.,'确定')]")
        sOk.click()
        sSave = driver.find_element_by_xpath("//a[contains(.,'保存')]")
        sSave.click()
        time.sleep(1)
        driver.switch_to.window(driver.window_handles[-1])
        '''
        sSendSignOff = driver.find_element_by_xpath("//a[contains(.,'送出签核')]")
        sSendSignOff.click()
        
        '''
        with open('./check.js', 'r',encoding='utf-8',errors='ignore') as f:
            str = f.read()
        print(str)
        driver.execute_script(str)
        time.sleep(3) # 一定要延时等这个弹窗出来
        alertObject = driver.switch_to.alert
        print(alertObject.text)  # 打印提示信息
        time.sleep(1)
        alertObject.accept()  # 点击确定按钮
        time.sleep(1)
        
    def __del__(self):
        driver = self.driver
        print("==> __del__()")
        #driver.quit()
''' 
if __name__ == '__main__':
   a =Spider()
   #a.signFiles()
   a.odering()
''' 
